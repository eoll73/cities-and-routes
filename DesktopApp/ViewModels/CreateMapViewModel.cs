﻿using DesktopApp.APIInteraction;
using DesktopApp.Dialogs.Commands;
using DesktopApp.Models;
using DesktopApp.Services;
using DesktopApp.Services.EventAggregator;
using GongSolutions.Wpf.DragDrop;
using Prism.Events;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels
{
    public enum AllowExtensions { jpg, png };
    internal class CreateMapViewModel : BaseViewModel, INotifyPropertyChanged, IDropTarget
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IOpenImageDialogService _openImageDialogService;
        private readonly IImageAPIService _imageAPIService;
        private readonly IMapAPIService _mapAPIService;
        private IEventAggregator _eventAggregator;

        public CreateMapViewModel(IMessageBoxService messageBoxService, IOpenImageDialogService openImageDialogService, IImageAPIService imageAPIService, IMapAPIService mapAPIService, IEventAggregator eventAggregator)
        {
            _messageBoxService = messageBoxService;
            _openImageDialogService = openImageDialogService;
            _imageAPIService = imageAPIService;
            _mapAPIService = mapAPIService;
            _eventAggregator = eventAggregator;
            InitializeProperties();
        }

        public void InitializeProperties(string name = "", string path = "")
        {
            NewMap = new Map() { Name = name };
            MapPath = path;
        }

        private Map newMap;
        public Map NewMap
        {
            get => newMap;
            set => Set(ref newMap, value, nameof(NewMap));
        }

        private string mapPath;
        public string MapPath
        {
            get => mapPath;
            set => Set(ref mapPath, value, nameof(MapPath));
        }

        #region CreateMapCommand

        public ICommand CreateMapCommand => new RelayCommand(async p => await OnCreateMapExecuted(p), p => OnCanCreateMapExecuted(p));

        private async Task OnCreateMapExecuted(object p)
        {
            try
            {
                NewMap.ImageId = await AddNewImage();
                NewMap.Id = await AddNewMap();
                _messageBoxService.ShowInfo($"We have a new map \"{NewMap.Name}\".", "Success");

                var res = await _mapAPIService.GetMapAsync(NewMap.Id);
                res.Payload.Image = new Image() { Data = await _imageAPIService.GetImageAsync(res.Payload.ImageId) };
                _eventAggregator.GetEvent<WholeMapSentEvent>().Publish(res.Payload);

                CloseWindowCommand.Execute(p);
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "An error occured. Please try it again.");
            }
        }

        private async Task<Guid> AddNewMap()
        {
            var res = await _mapAPIService.CreateMapAsync(NewMap);
            if (!res.IsSuccessful)
                throw new Exception("A map was not added.");
            else
                return res.Payload.Id;
        }

        private async Task<Guid> AddNewImage()
        {
            var res = await _imageAPIService.UploadImage(MapPath);
            if (!res.IsSuccessful)
                throw new Exception("An image was not added.");
            else
                return res.Payload;
        }

        private bool OnCanCreateMapExecuted(object p) => !string.IsNullOrEmpty(NewMap.Name) && !string.IsNullOrEmpty(MapPath);

        private bool CheckImage(string fullPath)
        {
            if (Enum.IsDefined(typeof(AllowExtensions), Path.GetExtension(fullPath).Trim('.').ToLower()))
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(fullPath);
                if (img.Width >= 1000 && img.Height >= 1000)
                {
                    return true;
                }
                else
                {
                    _messageBoxService.ShowError("Incorrect image size. Minimum size is 1000x1000.", "Error");
                }
            }
            else
            {
                _messageBoxService.ShowError("Incorrect image file.", "Error");
            }

            return false;
        }

        #endregion

        #region DragDropFile

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;

            var dataObject = dropInfo.Data as IDataObject;

            if (dataObject != null && dataObject.GetDataPresent(DataFormats.FileDrop))
            {
                dropInfo.Effects = DragDropEffects.Link;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            try
            {
                var dataObject = dropInfo.Data as DataObject;
                string[] dropPath = dataObject.GetData(DataFormats.FileDrop, true) as string[];

                string fullPath = Path.GetFullPath(dropPath[0]);

                if (CheckImage(fullPath))
                {
                    InitializeProperties(Path.GetFileNameWithoutExtension(dropPath[0]), fullPath);
                }
                else
                {
                    dropInfo.Effects = DragDropEffects.None;
                    return;
                }
            }
            catch (Exception ex)
            {
                _messageBoxService.ShowError(ex, "Some error here:");
            }
        }

        #endregion

        #region OpenFileDialogCommand

        public ICommand DownloadImageCommand => new RelayCommand(p => OnDownloadImageExecuted(p), p => OnCanDownloadImageExecuted(p));

        private void OnDownloadImageExecuted(object p)
        {
            var res = _openImageDialogService.ShowDialog();
            if (!String.IsNullOrEmpty(res))
            {
                if (CheckImage(res))
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(res);
                    InitializeProperties(Path.GetFileNameWithoutExtension(res), res);
                }
            }
        }

        private bool OnCanDownloadImageExecuted(object p) => true;

        #endregion

    }
}
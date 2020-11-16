﻿using DesktopApp.APIInteraction.Mapper;
using DesktopApp.Models;
using Service.DTO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApp.APIInteraction
{
    public class CityAPIService : ICityAPIService
    {
        public async Task<HttpResponsePayload<City>> CreateCityAsync(City city)
        {
            var cityDTO = AppMapper.GetAppMapper().Mapper.Map<CityCreateDTO>(city);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PostAsJsonAsync("city", cityDTO);
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }

            HttpResponsePayload<City> responsePayload = new HttpResponsePayload<City>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var cityGetDTO = await response.Content.ReadAsAsync<CityGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<City>(cityGetDTO);

            return responsePayload;
        }

        public async Task<HttpResponsePayload<City>> UpdateCityAsync(City city)
        {
            var cityDTO = AppMapper.GetAppMapper().Mapper.Map<CityCreateDTO>(city);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.PutAsJsonAsync("city/" + city.Id, cityDTO);
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }

            HttpResponsePayload<City> responsePayload = new HttpResponsePayload<City>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };
            var cityGetDTO = await response.Content.ReadAsAsync<CityGetDTO>();
            responsePayload.Payload = AppMapper.GetAppMapper().Mapper.Map<City>(cityGetDTO);

            return responsePayload;
        }


        public async Task<HttpResponsePayload<City>> DeleteCityAsync(City city)
        {
            var cityDTO = AppMapper.GetAppMapper().Mapper.Map<CityCreateDTO>(city);

            HttpResponseMessage response;

            try
            {
                response = await APIClient.Client.DeleteAsync("city/" + city.Id);
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }

            HttpResponsePayload<City> responsePayload = new HttpResponsePayload<City>()
            {
                IsSuccessful = response.IsSuccessStatusCode ? true : false
            };

            return responsePayload;
        }
    }
}
using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        Restaurant Add(Restaurant newRestaurant);
        int Commit();
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Cesar's Italy", Cuisine = CuisineType.Italian, Location = "Luxembourg" },
                new Restaurant { Id = 2, Name = "Mari's Mex", Cuisine = CuisineType.Mexican, Location = "Munich" },
                new Restaurant { Id = 3, Name = "Cho cho's Nepal", Cuisine = CuisineType.Indian, Location = "Munich" }
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant GetById(int id)
        {
            return restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return restaurants.Where(r => string.IsNullOrEmpty(name) || r.Name.StartsWith(name))
                              .OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var rest = restaurants.FirstOrDefault(r => r.Id == updatedRestaurant.Id);
            if (rest != null)
            {
                rest.Name = updatedRestaurant.Name;
                rest.Location = updatedRestaurant.Location;
                rest.Cuisine = updatedRestaurant.Cuisine;
            }
            return rest;
        }
    }
}

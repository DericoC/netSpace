using System;
using System.Collections.Generic;
using NetSpace.ViewModel;

namespace NetSpace.Model
{
    public class Place : BaseViewModel
    {
        public int place_id { get; set; }
        public Business business { get; set; }
        public string place_name { get; set; }
        public string description { get; set; }
        public string dimensions { get; set; }
        public int capacity { get; set; }
        public string amenities { get; set; }
        public string image { get; set; }
        public bool restrooms { get; set; }
        public User manager { get; set; }
        public Policy policy { get; set; }
        public int rating { get; set; }
        public List<Tags> tags { get; set; }
        public String tagsString { get; set; }

        public Place()
        {
        }

        public Place(int place_id, Business business, string place_name, string description, string dimensions, int capacity, string amenities, string image, bool restrooms, User manager, Policy policy, int rating, List<Tags> tags, string tagsString)
        {
            this.place_id = place_id;
            this.business = business;
            this.place_name = place_name;
            this.description = description;
            this.dimensions = dimensions;
            this.capacity = capacity;
            this.amenities = amenities;
            this.image = image;
            this.restrooms = restrooms;
            this.manager = manager;
            this.policy = policy;
            this.rating = rating;
            this.tags = tags;
            this.tagsString = tagsString;
        }
    }
}


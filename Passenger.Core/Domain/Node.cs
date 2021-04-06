using System;

namespace Passenger.Core.Domain
{
    public class Node
    {
        public string Address {get; protected set;}
        public double Longitude {get; protected set;}
        public double Latitude {get; protected set;}
        public DateTime UpdatedAt {get; protected set;}

        public Node()
        {
        }

        protected Node(string address, double longitude, double latitude)
        {
            SetAddress(address);
            SetLongitude(longitude);
            SetLatitude(latitude);

        }

        public void SetAddress(string address)
        {
            if(string.IsNullOrEmpty(address))
            {
                throw new Exception("Please enter valid address!");
            }
            if(Address == address)
            {
                return;
            }

            Address = address;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetLongitude(double longitude)
        {
            if(longitude < -180 || longitude > 180)
            {
                throw new Exception("This is not valid longitude!");
            }

            if(Longitude == longitude)
            {
                return;
            }

            Longitude = longitude;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetLatitude(double latitude)
        {
            if(latitude < -90 || latitude > 90) 
            {
                throw new Exception("This is not valid latitude!");
            }

            if(Latitude == latitude)
            {
                return;
            }

            Latitude = latitude;
            UpdatedAt = DateTime.UtcNow;
        }

        public static Node Create(string address, double longitude, double latitude)
            => new Node(address,longitude,latitude);
    }
}
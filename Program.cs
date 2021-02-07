using System;
using System.IO;
using System.Text;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace db
{
    class Program
    {
        static void Main()
        {
			JsonHandler jsonhandler = new JsonHandler();

			// do whatever, make object
			User user = new User {
				_id = 1,
				_username = "world"
			};

			Sale sale = new Sale {

				_id = 999,
				_amount = 9999
			};

			// get full string in format {event: <eventName>, payload:{inner: json, is: here}}
			string json = jsonhandler.OutgoingData("User", user);
			Console.WriteLine (json);

			// pass in string in above format to get back an object of appropriate type
			json = jsonhandler.OutgoingData("Sale", sale);
			Console.WriteLine (json);
			var obj2 = jsonhandler.IncomingData(json);
        }
    }

	public class JsonHandler
	{
		//
		/// object -> json string -> encoding -> packet (event: eventName, payload: encoded json string)
		///object -> toJson / return string -> encoding / return string -> packet / return string
		/// 
		/// string -> tuple<event, data> -> decode data -> deserialize string to object type based on event name
		/// 

		public string OutgoingData(string eventName, object obj)
		{
			// pass generic object to receive string
			string innerdata = ToJson (obj);
			//Console.WriteLine (innerdata);
			//encode string
			string encoded = ToBase64 (innerdata);

			Packet packet = new Packet {
				_event = eventName,
				_payload = encoded
			};

			// jsonify eventname + encoded
			string outerdata = ToJson(packet);
//			Console.WriteLine (outerdata);
			return outerdata;
		}
			
		public dynamic IncomingData(string data) 
		{
			var obj = FromJson (data);
			return obj;
		}
		
		public string ToJson (object obj)
		{
			return JsonConvert.SerializeObject (obj);
		}
			
		public dynamic FromJson(string data)
		{
			Packet packet = new Packet ();
			packet = JsonConvert.DeserializeObject<Packet> (data);

			string _type = packet._event;
			string _payload = DecodeBase64 (packet._payload);

			switch (_type) {
			// add case statements based on event name
			case "User":
				User user = new User ();
				user = JsonConvert.DeserializeObject<User> (_payload);
				return user;
			case "Sale":
				Sale sale = new Sale ();
				sale = JsonConvert.DeserializeObject<Sale> (_payload);
				return sale;
			default:
				Console.WriteLine ("event");
				return "default";
			}				
		}

		public string DecodeBase64(string encoded) 
		{
			var data = System.Convert.FromBase64String(encoded);
			return Encoding.UTF8.GetString (data);
		}

		public string ToBase64(string payLoad)
		{
			var payloadBytes = System.Text.Encoding.UTF8.GetBytes (payLoad);
			return System.Convert.ToBase64String (payloadBytes);
		}			
	}

	public class Packet
	{
		public string _event;
		public string _payload;
		public Sale _sale;
	}

	public class User
	{
		public int _id;
		public string _username;
	}

	public class Sale
	{
		public int _id;
		public int _amount;
	}

}

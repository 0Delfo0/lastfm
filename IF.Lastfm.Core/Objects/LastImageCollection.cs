﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace IF.Lastfm.Core.Objects
{
    public class LastImageCollection : IEnumerable<Uri>
    {
        public Uri Small { get; set; }
        public Uri Medium { get; set; }
        public Uri Large { get; set; }
        public Uri ExtraLarge { get; set; }

        private IEnumerable<Uri> Images
        {
            get
            {
                return new List<Uri>()
                       {
                           Small,
                           Medium,
                           Large,
                           ExtraLarge
                       };
            }
        }

        public static LastImageCollection ParseJToken(JToken images)
        {
            var c = new LastImageCollection();

            foreach (var image in images.Children())
            {
                var size = image.Value<string>("size");
                var uriString = image.Value<string>("#text");

                if (string.IsNullOrEmpty(uriString))
                {
                    break;
                }

                var uri = new Uri(uriString, UriKind.Absolute);

                switch (size)
                {
                    case "small":
                        c.Small = uri;
                        break;
                    case "medium":
                        c.Medium = uri;
                        break;
                    case "large":
                        c.Large = uri;
                        break;
                    case "extralarge":
                        c.ExtraLarge = uri;
                        break;
                }
            }

            return c;
        }

        public IEnumerator<Uri> GetEnumerator()
        {
            return Images.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
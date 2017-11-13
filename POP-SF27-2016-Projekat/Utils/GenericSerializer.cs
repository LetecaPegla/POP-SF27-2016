﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace POP_SF27_2016_Projekat.Utils
{
    public class GenericSerializer
    {
        #region ListSerializer
        public static void SerializeList<T>(string fileName, List<T> listToSerialize) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                using (var sw = new StreamWriter($@"../../Data/{fileName}"))
                {
                    serializer.Serialize(sw, listToSerialize);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Greska pri ispisu serijalizovanih podataka u {fileName}");
                throw;
            }
        }

        public static List<T> DeSerializeList<T>(string fileName) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(List<T>));
                using (var sr = new StreamReader($@"../../Data/{fileName}"))
                {
                    return (List<T>) serializer.Deserialize(sr);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Greska pri ucitavanju serijalizovanih podataka iz {fileName}");
                throw;
            }
        }
        #endregion

        #region ObjectSerializer
        public static void SerializeObject<T>(string fileName, object objectToSerialize) where T : class
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (var sw = new StreamWriter($@"../../Data/{fileName}"))
                {
                    serializer.Serialize(sw, objectToSerialize);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Greska pri ispisu serijalizovanih podataka u {fileName}");
                throw;
            }
        }

        public static T DeSerializeObject<T>(string fileName) where T : class
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (var sr = new StreamReader($@"../../Data/{fileName}"))
                {
                    return (T)serializer.Deserialize(sr);
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Greska pri ucitavanju serijalizovanih podataka iz {fileName}");
                throw;
            }
        }
        #endregion
    }
}

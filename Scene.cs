﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace FACELibrary
{
    [Serializable()]
    public class ObjectScene
    {
        private int Id;
        public int id
        {
            get { return Id; }
            set { Id = value; }
        }

        public ObjectScene() { }

        public ObjectScene(int IdObj)
        {
            id = IdObj;
        }

    }

    [Serializable()]
    public class Scene :IDisposable
    {
       
        private List<Subject> subjects;        
        public List<Subject> Subjects
        {
            get { return subjects; }
            set { subjects = value; }
        }

        private List<ObjectScene> objects;
        public List<ObjectScene> Objects
        {
            get { return objects; }
            set { objects = value; }
        }

        private  Surroundings environment;
        public Surroundings Environment
        {
            get { return environment; }
            set { environment = value; }
        }


        public Scene() { }


        public Scene(List<Subject> subjectsList, List<ObjectScene> objectsList, Surroundings environmentCont)
        {
           
            subjects = subjectsList;
            objects = objectsList;
            environment = environmentCont;
        }

        public string getstringOPC()
        {
            StringBuilder sbGeneric = new StringBuilder();

            sbGeneric.Append("(Subjects ");
            foreach (Subject subj in Subjects.ToList())
            {
                sbGeneric.Append("(");
                foreach (System.Reflection.PropertyInfo prop in typeof(Subject).GetProperties())
                {
                    object val = typeof(Subject).GetProperty(prop.Name).GetValue(subj, null);
                    if (val != null)
                    {
                        if (prop.PropertyType.IsGenericType)
                        {
                            System.Collections.IList l = (System.Collections.IList)val;
                            sbGeneric.AppendFormat(" ({0} ", prop.Name);
                            foreach (object elem in l)
                            {
                                if (elem.ToString() != null)
                                {
                                    if (elem.ToString().Length > 4 && prop.Name != "name")
                                        sbGeneric.AppendFormat("{0} ", elem.ToString().Substring(0, 4));
                                    else
                                        sbGeneric.AppendFormat("{0} ", elem.ToString());
                                }
                            }
                            sbGeneric.AppendFormat(")");
                        }
                        else
                        {
                            sbGeneric.AppendFormat(" ({0} {1})", prop.Name, val.ToString());
                        }
                    }
                }
                sbGeneric.Append(") ");

            }
            sbGeneric.Append(")");


            sbGeneric.Append(" (Object ");
            foreach (Object subj in Objects.ToList())
            {
                foreach (System.Reflection.PropertyInfo prop in typeof(Object).GetProperties())
                {
                    object val = typeof(Surroundings).GetProperty(prop.Name).GetValue(Objects, null);
                    if (val != null)
                    {
                        if (prop.PropertyType.IsGenericType)
                        {
                            System.Collections.IList l = (System.Collections.IList)val;
                            sbGeneric.AppendFormat(" ({0} ", prop.Name);
                            foreach (object elem in l)
                            {
                                if (elem.ToString() != null)
                                {
                                    if (elem.ToString().Length > 4)
                                        sbGeneric.AppendFormat("{0} ", elem.ToString().Substring(0, 4));
                                    else
                                        sbGeneric.AppendFormat("{0} ", elem.ToString());
                                }
                            }
                            sbGeneric.AppendFormat(")");
                        }
                        else
                        {
                            sbGeneric.AppendFormat("({0} {1})", prop.Name, val.ToString());
                        }
                    }

                }
            }
            sbGeneric.Append(")");

            sbGeneric.Append(" (Environment ");
            foreach (System.Reflection.PropertyInfo prop in typeof(Surroundings).GetProperties())
            {
                object val = typeof(Surroundings).GetProperty(prop.Name).GetValue(Environment, null);
                if (val != null)
                {
                    if (prop.PropertyType.IsGenericType)
                    {
                        System.Collections.IList l = (System.Collections.IList)val;
                        sbGeneric.AppendFormat(" ({0} ", prop.Name);
                        foreach (object elem in l)
                        {
                            if (elem.ToString() != null)
                            {
                                if (elem.ToString().Length > 4)
                                    sbGeneric.AppendFormat("{0} ", elem.ToString().Substring(0, 4));
                                else
                                    sbGeneric.AppendFormat("{0} ", elem.ToString());
                            }
                        }
                        sbGeneric.AppendFormat(")");
                    }
                    else
                    {
                        sbGeneric.AppendFormat(" ({0} {1})", prop.Name, val.ToString());
                    }
                }
            }
            sbGeneric.Append(")");


            return sbGeneric.ToString();

        }
       

        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        { 
            Dispose(true);
            GC.SuppressFinalize(this);           
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return; 

            if (disposing) {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~Scene()
        {
            Dispose();
        }
       
    }
}
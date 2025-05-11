using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BrainZap_Server.CLASSES
{
    public class ClPregunta
    {
        public string Texto { get; set; }
        public List<string> Opciones { get; set; }
        public int Correcta { get; set; }
    }

    public class ClPreguntas
    {
        private List<ClPregunta> preguntas;
        private int indiceActual = 0;

        public ClPreguntas(string rutaArchivoJson)
        {
            string json = File.ReadAllText(rutaArchivoJson);
            preguntas = JsonConvert.DeserializeObject<List<ClPregunta>>(json);
        }

        public ClPregunta ObtenerSiguiente()
        {
            if (indiceActual >= preguntas.Count) return null;
            return preguntas[indiceActual++];
        }

        public void Reset()
        {
            indiceActual = 0;
        }
    }
}

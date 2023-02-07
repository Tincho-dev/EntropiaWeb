using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models
{
    public class Fuente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IdFuente { get; set; }
        [Required]
        public List<Letra> Letras { get; set; } = new List<Letra>();
        public int N { get; set; }
        [Display(Name = "Cadena usada para generar la fuente: ")]
        public string CadenaFuente { get; set; } = "";
        public float EntropiaMaxima { get; set; }
        public string CadenaCodificada { get; set; } = string.Empty;
        public float EntropiaDeLaFuente { get; set; }

        public Fuente()
        {
            IdFuente = Guid.NewGuid().ToString();
        }

        public Fuente(string cadena)
        {
            //IdFuente = cadena.GetHashCode().ToString();
            CadenaFuente = cadena;
            Letras = StringToListLetra();
            EstablecerCodigoACadaLetra();
            N = Letras.Count;
            EntropiaMaxima = (float) (Math.Log10(N)/Math.Log10(2));
            CadenaCodificada = CodificarCadena();
            //revisar codigo de entropia de la fuente
            EntropiaDeLaFuente = CalcularEntropiaDeLaFuente();
        }

        public float InformacionDeCadena()
        {
            float suma = 0;
            foreach (Letra letra in Letras)
            {
                //informacion individual de cada simbolo
                suma += (float) (Math.Log(1 / letra.Probability)/Math.Log(2));
            }
            return suma;
        }

        public float CalcularEntropiaDeLaFuente()
        {
            float suma = 0;
            foreach (Letra letra in Letras)
            {
                //informacion individual de cada simbolo
                suma += letra.Probability * (float)(Math.Log(1 / letra.Probability) / Math.Log(2));
            }
            return suma;
        }

        public List<Letra> StringToListLetra()
        {
            //crea una lista para guardar las letras de la fuente
            List<Letra> l = new List<Letra>();
            //crea una cadena de caracteres que representa cada letra
            char[] s1 = CadenaFuente.ToCharArray();
            //guardamos los daots en una variable sin elementos repetidos
            var s2 = s1.Distinct().ToArray();
            // creamos una clase letra por cada letra en la cadena y la guardamos en la lista

            foreach (char item in s2)
            {
                int freq = s1.Count(f => (f == item));

                int total = s1.Length;
                double probabilidad = (double)freq / total;
                var letraAgregar = new Letra(item.ToString(), probabilidad, freq);
                letraAgregar.Id = letraAgregar.GetHashCode().ToString();
                l.Add(letraAgregar);
            }
            return l;
        }

        public string Modular()
        {
            string modulada = string.Empty;

            foreach (var item in CadenaCodificada)
            {
                if (item == '0')
                    modulada += "_";
                else
                    modulada += "-";
            }


            return modulada;
        }

        public string CodificarCadena()
        {
            string result = "";

            foreach (var item in CadenaFuente)
            {
                var codigo = (from l in Letras
                              where (l.Name == item.ToString())
                              select l.Codigo).Single();
                result += codigo.ToString();
            }

            return result;
        }

        public void OrdenarLetrasSegunProbabilidad()
        {
            Letras = Letras.OrderByDescending(l => l.Probability).ToList();
        }

        public void EstablecerCodigoACadaLetra()
        {
            OrdenarLetrasSegunProbabilidad();
            //convertimos la lista a un arreglo para tner acceso al numero de la posicion de cada elemento
            var LetrasArray = Letras.ToArray();

            for (int i = 0; i < LetrasArray.Length; i++)
            {
                //convertimos a un texto el numero i a base 2
                LetrasArray[i].Codigo = Convert.ToString(i, 2);
            }
            //devolvemos el valor a la lista con los nuevos valores de los codigos
            Letras = LetrasArray.ToList();
        }
    }
}
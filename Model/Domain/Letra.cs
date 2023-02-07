using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Letra
    {
        //clave primaria para la base de datos
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        //establecemos las propiedades de modo de "solo lectura" al no tener el Set para cambiar sus valores
        [Required]
        public string Name { get; set; }
        [Required]
        public float Probability { get; set; }
        public float Information { get; set; }
        public string Codigo { get; set; } = string.Empty; //el codigo es vacio, la fuente es quien luego lo establece
        public int FrecuenciaDeAparicion { get; set; }

        public string IdFuente { get; set; }
        [ForeignKey("IdFuente")]
        public virtual Fuente Fuente { get; set; }
        public Letra()
        {
            Id = Guid.NewGuid().ToString();
        }
        //recibimos el nombre y la probabilidad al crear la letra, la informacion se calcula en funcoin de la probabilidad
        public Letra(string name, double probability, int freq)
        {
            Name = name;
            this.Probability = (float)probability;
            //calculamos la informacion segun la ecuacion que depende de la probabilidad
            Information = (float) (Math.Log(1 / probability)/Math.Log(2));
            FrecuenciaDeAparicion = freq;
        }

        //public Letra(char name, double probability)
        //{
        //    Id = new Guid().ToString();
        //    Name = name;
        //    this.Probability = probability;
        //    //calculamos la informacion segun la ecuacion que depende de la probabilidad
        //    Information = Math.Log(1 / probability)/Math.Log(2);
        //}
    }
}
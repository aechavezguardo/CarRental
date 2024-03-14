namespace Domain
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Tipo { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
        public string Localizacion { get; set; }
        public virtual ICollection<Disponibilidad> Disponibilidad { get; set; }
    }
}

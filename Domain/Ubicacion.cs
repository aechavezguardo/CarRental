namespace Domain
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string Localidad { get; set; }
        public virtual ICollection<Disponibilidad> Disponibilidad { get; set; }
    }
}


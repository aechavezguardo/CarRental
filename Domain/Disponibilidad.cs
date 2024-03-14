namespace Domain
{
    public class Disponibilidad
    {
        public int Id { get; set; }
        public int IdVehiculo { get; set; }
        public int IdUbicacion { get; set; }
        public bool Disponible { get; set; }
        public virtual Ubicacion Ubicacion { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
    }
}

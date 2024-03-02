namespace DbClasses.Models
{
    public class DiagnosticsEntities
    {
        public int id { get; set; }
        public DateTime DateBegin { get; set; }
        public virtual PatientEntities patient { get; set; }
        public virtual DoctorEntities doctor { get; set; }
        public virtual EventEntities events { get; set; }
        public string results { get; set; }
        public string recomendations { get; set; }
    }
}
namespace DbClasses.Models
{
    public class DoctorEntities
    {
        public int id { get; set; }
        public virtual AccountEntities account {  get; set; }
        public virtual ProfessionEntity profession { get; set; }
    }
}

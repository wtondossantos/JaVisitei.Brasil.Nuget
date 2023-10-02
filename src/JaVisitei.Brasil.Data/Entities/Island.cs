namespace JaVisitei.Brasil.Data.Entities
{
    public partial class Island
    {
        public string Id { get; set; }
        public string ArchipelagoId { get; set; }
        public string Name { get; set; }
        public string Canvas { get; set; }

        public virtual Archipelago Archipelago { get; set; }
    }
}

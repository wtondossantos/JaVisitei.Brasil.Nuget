
namespace JaVisitei.Brasil.Business.Validation.Models
{
    public class ModelValidator<T>
    {
        public T? Data { get; set; }
        public IList<string> Errors { get; set; }
        public string Message { get; set; }
        public virtual bool IsValid => Errors.Count.Equals(0);

        public ModelValidator()
        {
            Errors = new List<string>();
            Message = string.Empty;
        }
    }
}

namespace MovieReviewsAPI.Interfaces
{
    public interface IEditableByOwner
    {
        public int? CreatedById { get; }
    }
}
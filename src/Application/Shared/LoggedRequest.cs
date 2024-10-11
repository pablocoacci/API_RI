namespace Application.Shared
{
    public abstract class LoggedRequest<T> : IRequesDecorator<T>
    {
        private string userId;

        public string GetUserName()
        {
            return userId;
        }

        public void SetUserName(string userId)
        {
            this.userId = userId;
        }

        public virtual bool SaveChanges()
            => false;
    }
}

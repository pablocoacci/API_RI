using MediatR;

namespace Application.Shared
{
    public interface IRequesDecorator<T> : IRequest<T>
    {
        //public bool SaveChanges { get; }
        public bool SaveChanges();
    }
}

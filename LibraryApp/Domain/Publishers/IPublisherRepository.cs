namespace Domain.Publishers;

public interface IPublisherRepository
{
    public List<Publisher> GetAll();
    public Publisher GetById(int id);
    public void Insert(Publisher publisher);
    public void Update(Publisher publisher);
    public void Delete(int id);
}
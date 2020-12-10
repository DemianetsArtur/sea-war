namespace SeaWar.DAL.Interfaces
{
    public interface IUoW
    {
        IPlayerRepository playerRepository { get; }
    }
}

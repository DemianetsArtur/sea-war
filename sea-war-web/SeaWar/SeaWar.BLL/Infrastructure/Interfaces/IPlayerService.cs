using SeaWar.BLL.Infrastructure.ModelsDto;
using System.Collections.Generic;

namespace SeaWar.BLL.Infrastructure.Interfaces
{
    public interface IPlayerService
    {
        void PlayerAdd(PlayerDto entity);

        ICollection<PlayerDto> GetAll();

        void PlayerHitPointUpdate(PlayerDto entity);

        void PlayerCountUpdate(PlayerDto entity);

        void PlayerCoordinateCreate(PlayerDto entity);
    }
}

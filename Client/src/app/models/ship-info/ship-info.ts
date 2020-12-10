import { Battleship } from '../ships/battleship/battleship';
import { Cruisers } from '../ships/cruisers/cruisers';
import { Destroyer } from '../ships/destroyer/destroyer';
import { TorpedoBoat } from '../ships/torpedoBoat/torpedo-boat';
export class ShipInfo {
    battleship: Battleship;
    cruiser: Cruisers;
    destroyer: Destroyer;
    torpedoBoat: TorpedoBoat;

    constructor(){
        this.torpedoBoat = new TorpedoBoat();
        this.destroyer = new Destroyer();
        this.cruiser = new Cruisers();
        this.battleship = new Battleship();
    }
}

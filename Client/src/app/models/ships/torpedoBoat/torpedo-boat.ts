import { Coordinate } from '../../coordinate/coordinate';

export class TorpedoBoat {
    name = 'TorpedoBoat';
    size = 1;
    count = 4;
    isState = true;
    isStateCount = 0;
    coordinates: Array<Coordinate> = [];
}

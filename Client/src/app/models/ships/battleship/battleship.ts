import { Coordinate } from '../../coordinate/coordinate';
export class Battleship {
    name = 'Battleship';
    size = 4;
    count = 1;
    isState = true;
    isStateCount = 0;
    coordinates: Array<Coordinate> = [];
}

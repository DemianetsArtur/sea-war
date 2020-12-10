import { Coordinate } from '../coordinate/coordinate';

export class Player {
    Name!: string;
    Coordinates!: Array<Coordinate>;
    HitPoints!: number;
    Count!: number;
}

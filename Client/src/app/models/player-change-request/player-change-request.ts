import { Coordinate } from '../coordinate/coordinate';

export class PlayerChangeRequest {
    Id!: number;
    Color!: string;
    Name!: string;
    Coordinates!: Array<Coordinate>;
}

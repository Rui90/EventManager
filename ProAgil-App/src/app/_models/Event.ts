import { Lot } from './Lot';
import { SocialNetwork } from './SocialNetwork';
import { Guest } from './Guest';

export interface Event {
    id: number;
    location: string;
    date: Date;
    theme: string;
    capacity: number;
    imageUrl: string;
    phoneNumber: string;
    email: string;
    lots: Lot[];
    socialNetworks: SocialNetwork[];
    guestsEvents: Guest[];
}

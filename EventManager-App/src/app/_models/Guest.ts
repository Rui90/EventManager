import { SocialNetwork } from './SocialNetwork';

export interface Guest {

    id: number;
    name: string;
    description: string;
    imageUrl: string;
    phoneNumber: string;
    email: string;
    socialNetworks: SocialNetwork[];
    guestsEvents: Event[];
}

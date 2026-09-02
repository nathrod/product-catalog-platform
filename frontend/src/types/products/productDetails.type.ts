export type ProductDetail = {
    id: string;
    name: string;
    description?: string;
    imageURL: string;
    rating: number;
    reviewCount: number;
    active: boolean;
}
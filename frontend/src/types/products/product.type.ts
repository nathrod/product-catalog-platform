import type { ProductCategory, ProductPriority } from "@/constants/enum";

export type Product = {
    id: string;
    code: string;
    name: string;
    description?: string;
    category: ProductCategory;
    price: number;
    isActive: boolean;
    priority: ProductPriority;
    imageURL?: string;
}

export type CreateProduct = Omit<Product, 'id' | 'imageURL'>;
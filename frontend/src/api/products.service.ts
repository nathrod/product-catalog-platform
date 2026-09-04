// GET    /products
// GET    /products/{id}
// POST   /products 
// PUT    /products
// DELETE /products/ 

import { api } from '@/config/axios'
import type { PagedResult } from '@/types/pageListResult.type';
import type { CreateProduct, Product } from '@/types/products/product.type';
import type { QueryCondition } from '@/types/query/queryCondition.type';

export default class ProductService {
    static async getAll(
        query: QueryCondition
    ): Promise<PagedResult<Product>> {
        const { data } = await api.post('/products/search', query);
        return data;
    }

    static async getById(id: string): Promise<Product> {
        const { data } = await api.get(`/products/${id}`);
        return data;
    }

    static async create(
        payload: CreateProduct,
        image?: File
    ): Promise<Product> {
        const formData = new FormData();
        
        formData.append('code', payload.code);
        formData.append('name', payload.name);
        formData.append('price', String(payload.price));
        formData.append('category', String(payload.category));
        formData.append('priority', String(payload.priority));
        formData.append('isActive', String(payload.isActive));

        if (payload.description)
        {
            formData.append('description', payload.description);
        }

        if(image)
        {
            formData.append('image', image);
        }

        const { data } = await api.post('/products', formData);
        return data;
    }

    static async update(
        payload: Product, 
        image?: File
    ): Promise<Product> {
        const formData = new FormData();
        
        formData.append('id', payload.id);
        formData.append('code', payload.code);
        formData.append('name', payload.name);
        formData.append('price', String(payload.price));
        formData.append('category', String(payload.category));
        formData.append('priority', String(payload.priority));
        formData.append('isActive', String(payload.isActive));

        if (payload.description)
        {
            formData.append('description', payload.description);
        }

        if (image) 
        {
            formData.append('image', image);
        }
        const { data } = await api.put('/products', formData);
        return data;
    }

    static async delete(ids: string[]): Promise<void> {
        await api.delete('/products', {
            data: ids
        });
    }
}
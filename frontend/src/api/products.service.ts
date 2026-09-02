// GET    /products
// GET    /products/{id}
// POST   /products 
// PUT    /products
// DELETE /products/ 

// A função deverá:

// Receber um QueryCondition.
// Fazer GET /products
// Enviar pageSize, pageIndex, filters, sorts etc.
// Retornar o resultado tipado.

// import axios from "axios";

// const response = await axios.get(
//     // baseURL+/api/Products
//     "http://localhost:5120/api/Products",
//     {timeout: 5000,}
// );

// console.log(response.data);

import { api } from '../config/axios'
import type { PagedResult } from '../types/pageListResult.type';
import type { CreateProduct, Product } from '../types/products/product.type';
import type { QueryCondition } from '../types/query/queryCondition.type';

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

    static async create(payload: CreateProduct): Promise<Product> {
        const { data } = await api.post('/products', payload);
        return data;
    }

    static async update(payload: Product): Promise<Product> {
        const { data } = await api.put('/products', payload);
        return data;
    }

    //Recebe uma lista de ids do body e retorna NoContent()
    static async delete(ids: string[]): Promise<void> {
        await api.delete('/products', {
            data: ids
        });
    }
}
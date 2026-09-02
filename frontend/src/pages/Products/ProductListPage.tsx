//Home page, the page responsible for listing the products
// ProductListPage

// import { useEffect, useState } from "react";
// import type { Product } from "../../types/product.type";
// import type { QueryCondition } from "../../types/query/queryCondition.type";
// import ProductService from "../../api/products.service";
// import { Table } from "antd";
import { Button } from "antd";
import { productFilters } from "./components/productFilters";
import ProductTable from "./components/ProductTable";

export default function ProductListPage() {
    // const [products, setProducts] = useState<Product[]>([]);
    // const [loading, setLoading] = useState(true);
    
    // const [query, setQuery] = useState<QueryCondition>({
    //     pageSize: 10,
    //     pageIndex: 1,
    // });

    // useEffect(() => {
    //     const loadProducts = async () => {
    //         try{
    //             const result = await ProductService.getAll(query);
    //             setProducts(result.items);
    //         } catch (error) {
    //             console.error('Error loading products:', error);
    //         } finally {
    //             setLoading(false);
    //         }
    //     };

    //     loadProducts();
    // }, [query]);

    // const handlePageChange = (page: number) => {
    //     setQuery({
    //         ...query,
    //         pageIndex: page,
    //     });
    // };

    return (
        <div className="p-6"> 
            <h1 className="mb-4 text 2xl font-semibold">
                Products
            </h1>

            <Button type="primary">Add New Product</Button>

            <ProductTable />

            {/* posso passar como props a tabela
            <Table<Product>
                rowKey="id"
                loading={loading}
                dataSource={products}
                columns={[
                    {
                        title: 'Code',
                        dataIndex: 'code'
                    },
                    {
                        title: 'Name',
                        dataIndex: 'name'
                    },
                    {
                        title: 'Price',
                        dataIndex: 'price'
                    },
                    {
                        title: 'Category',
                        dataIndex: 'category'
                    },
                    {
                        title: 'Priority',
                        dataIndex: 'priority'
                    },
                    {
                        title: 'Available',
                        dataIndex: 'active'
                    }
                ]}
            /> */}
        </div>
    );
}

//Tem uma pagina principal que agarra essas outras paginas, então tem um layout por fora e ai cria as outras paginas meio que dentro
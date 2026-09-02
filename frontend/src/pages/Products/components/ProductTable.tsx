import { useEffect, useState } from "react";
import type { Product } from "../../../types/products/product.type";
import type { QueryCondition } from "../../../types/query/queryCondition.type";
import ProductService from "../../../api/products.service";
import { Table } from "antd";
import FilterBar from "../../../components/FilterBar";
import ProductFilters from "./ProductFilter";

export default function ProductTable() {

    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(false);
    const [total, setTotal] = useState(0);

    const [query, setQuery] = useState<QueryCondition>({
        pageSize: 10,
        pageIndex: 1,
    });
    
    useEffect(() => {
        const loadProducts = async () => {
            try{
                setLoading(true);
                const result = await ProductService.getAll(query);
                setProducts(result.items);
                setTotal(result.total);
            } catch (error) {
                console.error('Error loading products:', error);
            } finally {
                setLoading(false);
            }
        };
        loadProducts();
    }, [query]);
    
    const columns = [
        {
            key: '1',
            title: 'Code',
            dataIndex: 'code',
            sorter: true,
        },
        {
            key: '2',
            title: 'Name',
            dataIndex: 'name', 
            sorter: true,
        },
        {
            key: '3',
            title: 'Price',
            dataIndex: 'price', 
            sorter: true,
        },
        {
            key: '4',
            title: 'Category',
            dataIndex: 'category', 
            sorter: true,
        },
        {
            key: '5',
            title: 'Priority',
            dataIndex: 'priority',
            sorter: true, 
        },
        {
            key: '6',
            title: 'Available',
            dataIndex: 'isActive',
            render:(isActive: boolean)=>{
                return <p>{isActive?'Available':'Out of Stock'}</p>
            }
        },
    ]

    return (
        <div className="p-6">
            <div className="rounded-lg border border-gray-200 overflow-hidden bg-white [&_.ant-table]:rounded-none [&_.ant-table-container]:rounded-none">
            <FilterBar>
                <ProductFilters />
            </FilterBar>

            <div className="border-t border-gray-200">

            <Table<Product>
                rowKey="id"
                loading={loading}
                dataSource={products}
                columns={columns}
                pagination={{
                    current: query.pageIndex,
                    pageSize: query.pageSize,
                    total: total,
                    showTotal: (total) => `Total ${total} items`,
                }}
                onChange={(pagination, _filters, sorter)=>{
                    const sorterInfo = Array.isArray(sorter) ? sorter[0] : sorter;
                    
                    setQuery({
                        ...query,
                        pageSize: pagination.pageSize ?? 10,
                        pageIndex: pagination.current ?? 1,
                        sorts: sorterInfo?.order
                            ?[{
                                fieldName: sorterInfo.field as string,
                                descending: sorterInfo.order === "descend",
                            }]
                            : [],
                    });
                }}
            />
                </div>
            </div>
        </div>
    )
}
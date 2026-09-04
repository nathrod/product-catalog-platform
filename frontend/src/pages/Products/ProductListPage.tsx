import { useState } from 'react'
import { Button } from "antd"
import { PlusOutlined } from '@ant-design/icons'
import ProductTable from './components/ProductTable'
import ProductDrawer from './components/ProductDrawer'
import ProductDetailsModal from './components/ProductDetailsModal'
import type { Product } from '@/types/products/product.type'
import ProductService from '@/api/products.service'

export default function ProductListPage() {
    const [drawerOpen, setDrawerOpen] = useState(false)
    const [modalOpen, setModalOpen] = useState(false);
    const [selectedProduct, setSelectedProduct] = useState<Product | null>(null)
    const [productDetails, setProductDetails] =
    useState<Product | null>(null)
    const [refreshKey, setRefreshKey] = useState(0);

    const handleRefresh = () => {
        setRefreshKey((current) => current + 1);
    };

    const handleEdit = (product: Product) => {
        setSelectedProduct(product)
        setDrawerOpen(true)
    };

    const handleCreate = () => {
        setSelectedProduct(null)
        setDrawerOpen(true)
    }

    const handleDetails = async (id: string) => {
        try {
            const product = await ProductService.getById(id)

            setProductDetails(product)
            setModalOpen(true)
        } catch (error) {
            console.error('Error loading product details:', error)
        }
    }

    return (
        <div className="flex h-full min-h-0 flex-col"> 
            <div className="mb-4 grid grid-cols-3 items-center">
                <h1 className=" text-2xl font-semibold">
                    Products
                </h1>

                <div className="flex justify-center">
                    <Button 
                    type="primary" 
                    onClick={handleCreate}
                    icon={<PlusOutlined />}
                    >
                        Add New Product
                    </Button>
                </div>
            </div>

            <div className="min-h-0 flex-1">
                <ProductTable 
                    refreshKey={refreshKey} 
                    onRefresh={handleRefresh}
                    onEdit={handleEdit}
                    onVisualize={handleDetails}
                />
            </div>

            <ProductDrawer
                open={drawerOpen}
                product={selectedProduct}
                onClose={() => setDrawerOpen(false)}
                onCreated={handleRefresh}
            />

            <ProductDetailsModal
                open={modalOpen}
                product={productDetails}
                onClose={() => {
                    setModalOpen(false)
                    setProductDetails(null)
                }}
            />
        </div>
    );
}
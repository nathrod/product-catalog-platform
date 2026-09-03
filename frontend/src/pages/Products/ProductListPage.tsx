import { useState } from 'react'
import { Button } from "antd"
import { PlusOutlined } from '@ant-design/icons'
import ProductTable from "./components/ProductTable"
import ProductDrawer from './components/ProductDrawer'

export default function ProductListPage() {
    const [drawerOpen, setDrawerOpen] = useState(false)
    const [refreshKey, setRefreshKey] = useState(0);

    const handleProductCreated = () => {
        setRefreshKey((current) => current + 1);
    };

    return (
        <div className="flex h-full min-h-0 flex-col"> 
            <div className="mb-4 grid grid-cols-3 items-center">
                <h1 className=" text-2xl font-semibold">
                    Products
                </h1>

                <div className="flex justify-center">
                    <Button type="primary" onClick={() => setDrawerOpen(true)} icon={<PlusOutlined />}>
                        Add New Product
                    </Button>
                </div>
            </div>

            <div className="min-h-0 flex-1">
                <ProductTable refreshKey={refreshKey} />
            </div>

            <ProductDrawer
                open={drawerOpen}
                onClose={() => setDrawerOpen(false)}
                onCreated={handleProductCreated}
            />
        </div>
    );
}
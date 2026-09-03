import { Button } from "antd";
import ProductTable from "./components/ProductTable";

export default function ProductListPage() {
    return (
        <div className="flex h-full min-h-0 flex-col"> 
            <div className="mb-4 grid grid-cols-3 items-center">
                <h1 className=" text-2xl font-semibold">
                    Products
                </h1>

                <div className="flex justify-center">
                    <Button type="primary">
                    Add New Product
                    </Button>
                </div>
            </div>

            <div className="min-h-0 flex-1">
                <ProductTable />
            </div>
        </div>
    );
}

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
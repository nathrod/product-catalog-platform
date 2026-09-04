import { useEffect, useState } from 'react'
import { Button, Col, Drawer, Form, Input, InputNumber, message, Radio, Row, Select, Space, Upload, type UploadFile } from 'antd';

import { PlusOutlined } from '@ant-design/icons';

import { 
    ProductCategoryLabels, 
    ProductPriorityLabels, 
    ProductPriorityValues 
} from '@/constants/enum';

import ProductService from '@/api/products.service';
import type { CreateProduct, Product } from '@/types/products/product.type';

type ProductDrawerProps = {
  open: boolean
  onClose: () => void
  onCreated: () => void;
  product?: Product | null
};

export default function ProductDrawer({
  open,
  onClose,
  onCreated,
  product,
}: ProductDrawerProps) {
    const [form] = Form.useForm<CreateProduct>();
    const [loading, setLoading] = useState(false);
    const [fileList, setFileList] = useState<UploadFile[]>([])

    const isEditing = !!product

    useEffect(() => {
        if(!open) return
        if (product) {
            form.setFieldsValue({
                code: product?.code,
                name: product?.name,
                description: product?.description,
                category: product?.category,
                price: product?.price,
                isActive: product?.isActive,
                priority: product?.priority,
            })

        return
        }
        form.resetFields()
    }, [open, product, form])

    const handleSubmit = async () => {
        try {
            const values = await form.validateFields();
            setLoading(true);

            const imageFile = fileList[0]?.originFileObj

            if(product){
                await ProductService.update(
                    {
                        ...product,
                        ...values,
                    },
                    imageFile
                )
            } else {
                await ProductService.create(
                    {
                        ...values,
                        isActive: true,
                    },
                    imageFile
                )
            }

            message.success(
                product
                    ? 'Product updated successfully'
                    : 'Product created successfully'
            )

            form.resetFields();
            setFileList([])
            onClose();
            onCreated();
        } catch ( error ) {
            if (
                typeof error === 'object' && 
                error !== null &&
                'errorFields' in error
            ) {
                return
            }
            console.error(
                product
                    ? 'Error updating product: '
                    : 'Error creating product:',
                error
            )
            message.error(
                product
                 ? 'Failed to update product'
                 : 'Failed to create product'
            )
        } finally {
            setLoading(false);
        }
    };

    const handleClose = () => {
        form.resetFields()
        setFileList([])
        onClose()
    }

    return (
        <>
            <Drawer
                title={
                    isEditing
                        ? 'Edit product'
                        : 'Create a new product'
                }
                size={500}
                open={open}
                onClose={handleClose}
                styles={{
                    body: {
                        paddingBottom: 80,
                    },
                }}
                extra={
                    <Space>
                        <Button onClick={onClose}>
                            Cancel
                        </Button>
                        <Button 
                            type='primary' 
                            loading={loading} 
                            onClick={handleSubmit}
                        >
                            {isEditing ? 'Save' : 'Submit'}
                        </Button>
                    </Space>
                }
            >
                <Form<CreateProduct> 
                form={form} 
                layout='vertical' 
                requiredMark={false}
                initialValues={{
                    priority: ProductPriorityValues.Low,
                    isActive: true,
                }}
                >
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item
                                name="name"
                                label="Name"
                                rules={[{ required: true, message: 'Please enter product name'}]}
                            >
                                <Input placeholder='Please enter product name' />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                name="code"
                                label="Code"
                                rules={[{ required: true, message: 'Please enter code'}]}
                            >
                                <Input placeholder='Product code' />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                name="price"
                                label="Price"
                                className="w-full"
                                rules={[{ required: true, message: 'Please enter product price'}]}
                            >
                                <InputNumber placeholder='Price' min={0} className='w-full'/>
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={12}>
                            <Form.Item
                                name="category"
                                label="Category"
                                rules={[{ required: true, message: 'Please choose a category'}]}
                            >
                                <Select
                                    placeholder="Select a Category"
                                    options={Object.entries(ProductCategoryLabels).map(([value, label]) => ({
                                        label,
                                        value: Number(value),
                                    }))}
                                />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                        <Form.Item
                            name="priority"
                            label="Priority"
                            rules={[{ required: true, message: 'Choose product priority'}]}
                        >
                            <Radio.Group 
                                block 
                                options={Object.entries(ProductPriorityLabels).map(([value, label]) => ({
                                    label,
                                    value: Number(value),
                                }))}
                                optionType="button"
                                buttonStyle="solid"
                                >
                            </Radio.Group>
                        </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                            <Form.Item label="Product Image">
                                {product?.imageURL && (
                                    <div className='mb-3'>
                                        <p className='mb-2 text-sm text-gray-500'>
                                            Current image
                                        </p>

                                        <img
                                            src={product.imageURL}
                                            alt={product.name}
                                            className='h-24 w-24 rounded-md object-cover'
                                        />
                                    </div>
                                )}
                                <Upload 
                                    accept="image/*"
                                    listType="picture-card"
                                    fileList={fileList}
                                    maxCount={1}
                                    beforeUpload={(file) => {
                                    const isImage =
                                        file.type.startsWith('image/')

                                    if (!isImage) {
                                        message.error(
                                            'You can only upload image files'
                                        )

                                        return Upload.LIST_IGNORE
                                    }
                                    const isLt5MB = file.size / 1024 / 1024 < 5
                                    if (!isLt5MB) {
                                        message.error(
                                            'Image must be smaller than 5MB'
                                        )

                                        return Upload.LIST_IGNORE
                                    }

                                    return false
                                }}
                                onChange={({ fileList }) => {
                                    setFileList(fileList)
                                }}
                                onRemove={() => {
                                    setFileList([])
                                }}
                            >
                                {fileList.length === 0 && (
                                    <div>
                                        <PlusOutlined />

                                        <div className="mt-2">
                                            {product?.imageURL
                                                ? 'Replace'
                                                : 'Upload'}
                                        </div>
                                    </div>
                                )}
                                </Upload>
                            </Form.Item>
                        </Col>
                    </Row>
                    <Row gutter={16}>
                        <Col span={24}>
                        <Form.Item
                            name='description'
                            label='Description'
                        >
                            <Input.TextArea rows={4} />
                        </Form.Item>
                        </Col>

                    </Row>
                </Form>
            </Drawer>
        </>
    );
};
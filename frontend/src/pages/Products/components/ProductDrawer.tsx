import React, { useState } from 'react'
import { Button, Col, Drawer, Form, Input, InputNumber, Radio, Row, Select, Space, } from 'antd';
import { ProductCategoryLabels, ProductPriorityLabels, ProductPriorityValues } from '@/constants/enum';

import ProductService from '@/api/products.service';
import type { CreateProduct } from '@/types/products/product.type';

type ProductDrawerProps = {
  open: boolean
  onClose: () => void
  onCreated: () => void;
};

const ProductDrawer: React.FC<ProductDrawerProps> = ({
  open,
  onClose,
  onCreated,
}) => {
    const [form] = Form.useForm<CreateProduct>();
    const [loading, setLoading] = useState(false);

    const handleSubmit = async () => {
        try {
            const values = await form.validateFields();
            setLoading(true);
            const payload: CreateProduct = {
                ...values,
                isActive: true,
            };
            await ProductService.create(payload);
            form.resetFields();
            onClose();
            onCreated();
        } catch ( error ) {
            //mostrar uma mensagem na tela
            console.error('Error creating product:', error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <>
            <Drawer
                title="Create a new product"
                size={720}
                onClose={onClose}
                open={open}
                styles={{
                    body: {
                        paddingBottom: 80,
                    },
                }}
                extra={
                    <Space>
                        <Button onClick={onClose}>Cancel</Button>
                        <Button type='primary' loading={loading} onClick={handleSubmit}>Submit</Button>
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
                                <InputNumber placeholder='Price' />
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
                                defaultValue={ProductPriorityValues.Low}
                                optionType="button"
                                buttonStyle="solid"
                                >
                            </Radio.Group>
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

export default ProductDrawer;
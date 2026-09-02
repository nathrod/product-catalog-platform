export const ProductCategoryValues = {
    Electronics: 1,
    ClothingAndApparel: 2,
    HomeAndGarden: 3,
    BooksAndMedia: 4,
    HealthAndBeauty: 5,
    ToysAndGames: 6,
    SportsAndOutdoors: 7,
    Automotive: 8,
    GroceryAndFood: 9,
    PetSupplies: 10,
    NoCategorized: 11,
} as const;

export type ProductCategory = typeof ProductCategoryValues[keyof typeof ProductCategoryValues];

export const ProductPriorityValues = {
    Low: 1,
    Medium: 2,
    High: 3,
} as const;

export type ProductPriority = typeof ProductPriorityValues[keyof typeof ProductPriorityValues];

export const ProductCategoryLabels: Record<ProductCategory, string> = {
  [ProductCategoryValues.Electronics]: "Eletrônicos",
  [ProductCategoryValues.ClothingAndApparel]: "Roupas e Vestuário",
  [ProductCategoryValues.HomeAndGarden]: "Casa e Jardim",
  [ProductCategoryValues.BooksAndMedia]: "Livros e Mídia",
  [ProductCategoryValues.HealthAndBeauty]: "Saúde e Beleza",
  [ProductCategoryValues.ToysAndGames]: "Brinquedos e Jogos",
  [ProductCategoryValues.SportsAndOutdoors]: "Esportes e Lazer",
  [ProductCategoryValues.Automotive]: "Automotivo",
  [ProductCategoryValues.GroceryAndFood]: "Supermercado e Alimentos",
  [ProductCategoryValues.PetSupplies]: "Artigos para Pets",
  [ProductCategoryValues.NoCategorized]: "Sem Categoria",
};

export const ProductPriorityLabels: Record<ProductPriority, string> = {
  [ProductPriorityValues.Low]: "Baixa",
  [ProductPriorityValues.Medium]: "Média",
  [ProductPriorityValues.High]: "Alta",
};
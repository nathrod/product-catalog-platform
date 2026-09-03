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
  [ProductCategoryValues.Electronics]: "Electronics",
  [ProductCategoryValues.ClothingAndApparel]: "Clothing",
  [ProductCategoryValues.HomeAndGarden]: "Home and Garden",
  [ProductCategoryValues.BooksAndMedia]: "Books", 
  [ProductCategoryValues.HealthAndBeauty]: "Health and Beauty",
  [ProductCategoryValues.ToysAndGames]: "Toys and Games",
  [ProductCategoryValues.SportsAndOutdoors]: "Sports and Outdoors",
  [ProductCategoryValues.Automotive]: "Automotive",
  [ProductCategoryValues.GroceryAndFood]: "Grocery and Food",
  [ProductCategoryValues.PetSupplies]: "Pet Supplies",
  [ProductCategoryValues.NoCategorized]: "No Categorized",
};

export const ProductPriorityLabels: Record<ProductPriority, string> = {
  [ProductPriorityValues.Low]: "Low",
  [ProductPriorityValues.Medium]: "Medium",
  [ProductPriorityValues.High]: "High",
};
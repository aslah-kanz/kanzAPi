# Kanzway Meilisearch configuration

# Indexes

- Families
- Products

## Families Configuration

### Attributes

| Setting | Value |
| ------- | ----- |
| Searchable attributes          |   ["familyCode","namesEn","namesAr","brand","mpns","categories","priceSummaries","specificationValuesEn","specificationValuesAr"]         |
| Filterable attributes               |  ["brand","categories","familyCode","priceSummaries","specificationValuesAr","specificationValuesEn"]          |
| Sortable attributes              |  ["priceSummaries.AE.price.lowestPrice","priceSummaries.ID.price.lowestPrice","priceSummaries.MY.price.lowestPrice","priceSummaries.QA.price.lowestPrice","priceSummaries.SA.price.lowestPrice"]         |
| Displayed attributes              |  ["*"]         |
| Distinct attribute             |  ["*"]         |

### Rangking rules

| Setting | Value |
| ------- | ----- |
| rules          |   ["words","typo","proximity","attribute","sort","exactness"]        |
| Proximity rule options         |   By Word       |

### Pagination

| Setting | Value |
| ------- | ----- |
| maxTotalHits        |  10000       |

### Faceting

| Setting | Value |
| ------- | ----- |
| Max value per facet|  100       |

## Product Configuration

### Attributes

| Setting | Value |
| ------- | ----- |
| Searchable attributes          |   ["familyCode","category","mpn","gtin","name","brand","specificationValuesEn","specificationValuesAr"]        |
| Filterable attributes               |  ["brand","category","familyCode","gtin","mpn","name","priceSummaries.AE.price.lowestPrice","priceSummaries.ID.price.lowestPrice","priceSummaries.MY.price.lowestPrice","priceSummaries.QA.price.lowestPrice","priceSummaries.SA.price.lowestPrice","specificationValuesAr","specificationValuesEn"]        |
| Sortable attributes              |  ["priceSummaries.AE.price.lowestPrice","priceSummaries.ID.price.lowestPrice","priceSummaries.MY.price.lowestPrice","priceSummaries.QA.price.lowestPrice","priceSummaries.SA.price.lowestPrice"]        |
| Displayed attributes              |  ["*"]         |
| Distinct attribute             |  ["*"]         |

### Rangking rules

| Setting | Value |
| ------- | ----- |
| rules          |   ["words","typo","proximity","attribute","sort","exactness"]        |
| Proximity rule options         |   By Word       |

### Pagination

| Setting | Value |
| ------- | ----- |
| maxTotalHits        |  1000       |

### Faceting

| Setting | Value |
| ------- | ----- |
| Max value per facet|  100       |

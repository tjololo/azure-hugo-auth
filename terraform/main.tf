terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "2.47.0"
    }
  }
  backend "azurerm" {
    resource_group_name  = "hugo"
    storage_account_name = "hugotf"
    container_name       = "terraform-state"
    key                  = "hugo.tfstate"
  }
}

provider "azurerm" {
  # Configuration options
  features {}
}

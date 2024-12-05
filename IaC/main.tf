terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "4.11.0" # Use version 3.x.x but not 4.0.0 or higher
    }
  }
  required_version = ">= 1.1.0" # Ensures Terraform CLI version compatibility
}


provider "azurerm" {
  features {}
  subscription_id = "96a13dc7-6d34-4771-81e6-16939d91185c"
}

resource "azurerm_resource_group" "messaging-test" {
  name     = "messaging-rg"
  location = "Australia Central"
}

resource "azurerm_servicebus_namespace" "messaging-test" {
  name                = "messaging-servicebus-namespace"
  location            = azurerm_resource_group.messaging-test.location
  resource_group_name = azurerm_resource_group.messaging-test.name
  sku                 = "Standard"

  tags = {
    source = "terraform"
  }
}

resource "azurerm_servicebus_queue" "messaging-test" {
  name         = "messaging_servicebus_queue"
  namespace_id = azurerm_servicebus_namespace.messaging-test.id

  partitioning_enabled = true
}

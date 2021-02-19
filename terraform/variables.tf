variable "location" {
  description = "Location to create resources"
  default = "East US"
}

variable "resourcegroup" {
  description = "resourcegroup to use"
}

variable "storageaccountname" {
    description = "name of storageaccount"
}

variable "proxyname" {
    description = "Name of the auth function"
}
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

variable "google_client_id" {
  description = "google auth id"
}

variable "google_client_secret" {
  description = "google auth secret"
}

variable "allowed_users" {
  description = "comma separated list of allowed users"
  default = ""
}
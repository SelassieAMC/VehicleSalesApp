terraform {
  cloud {
    organization = "TrainingGA"

    workspaces {
      name = "gh-actions-demo"
    }
  }

  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~>4.31.0"
    }
  }

  required_version = ">= 1.2.0"
}

provider "aws" {
  region  = "us-east-1"
}

resource "aws_elastic_beanstalk_application" "sampleapp" {
  name           = "SampleApp"
}


resource "aws_elastic_beanstalk_environment" "beanstalkappenv" {
  name                = "default_env"
  application         = aws_elastic_beanstalk_application.sampleapp.name
}

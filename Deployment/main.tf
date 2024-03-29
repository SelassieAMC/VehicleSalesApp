terraform {
  cloud {
    organization = "TrainingGA"

    workspaces {
      name = "VehicleSalesApp"
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
  name                = "my-training-environment"
  application         = aws_elastic_beanstalk_application.sampleapp.name
  solution_stack_name   = "64bit Amazon Linux 2 v2.3.5 running .NET Core"

  setting {
        namespace = "aws:autoscaling:launchconfiguration"
        name      = "IamInstanceProfile"
        value     = "aws-elasticbeanstalk-ec2-role"
      }
}

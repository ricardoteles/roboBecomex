import {MatSnackBar } from '@angular/material/snack-bar';
import { RobotService } from './../shared/services/robot.service';
import { Component, OnInit } from '@angular/core';
import { ApiRobotResponse } from '../shared/response/apiRobotResponse.model';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
  providers: [ RobotService ]
})
export class MainComponent implements OnInit {
  estadoAtualRobo!: ApiRobotResponse;
  selectedElbowStrenght = '1';
  selectElbow = 'left';
  selectedWristRotation = '3';
  selectedWrist = 'left';
  selectedHeadRotation = '3';
  selectedHeadInclination = '2';

  constructor(private robotService: RobotService, protected snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.robotService.getInitialRobotState().subscribe((resp: ApiRobotResponse) => {
      this.estadoAtualRobo = resp;
    });
  }

  moveRobotElbow() {
    this.robotService.moveRobotElbow(this.selectElbow, this.selectedElbowStrenght).subscribe((resp: ApiRobotResponse) => {
      this.estadoAtualRobo = resp;
      if(this.estadoAtualRobo.errorMsg) {
        this.showSnackBarErrorMsg(this.estadoAtualRobo.errorMsg);
      }
    },
    (erro: any) => {
      console.log(erro);
    });
  };

  rotateRobotWrist() {
    this.robotService.rotateRobotWrist(this.selectedWrist, this.selectedWristRotation).subscribe((resp: ApiRobotResponse) => {
      this.estadoAtualRobo = resp;
      if(this.estadoAtualRobo.errorMsg) {
        this.showSnackBarErrorMsg(this.estadoAtualRobo.errorMsg);
      }
    },
    (erro: any) => {
      console.log(erro);
    });
  };

  moveRobotHead() {
    this.robotService.moveRobotHead(this.selectedHeadInclination).subscribe((resp: ApiRobotResponse) => {
      this.estadoAtualRobo = resp;
      if(this.estadoAtualRobo.errorMsg) {
        this.showSnackBarErrorMsg(this.estadoAtualRobo.errorMsg);
      }
    },
    (erro: any) => {
      console.log(erro);
    });
  };

  rotateRobotHead() {
    this.robotService.rotateRobotHead(this.selectedHeadRotation).subscribe((resp: ApiRobotResponse) => {
      this.estadoAtualRobo = resp;
      if(this.estadoAtualRobo.errorMsg) {
        this.showSnackBarErrorMsg(this.estadoAtualRobo.errorMsg);
      }
    },
    (erro: any) => {
      console.log(erro);
    });
  };

  onSelectedElbowStrenght(value:string): void {
		this.selectedElbowStrenght = value;
	}

  onSelectedElbow(value:string): void {
		this.selectElbow = value;
	}

  onSelectedWristRotation(value:string): void {
		this.selectedWristRotation = value;
	}

  onSelectedWrist(value:string): void {
		this.selectedWrist = value;
	}

  onSelectedHeadRotation(value:string): void {
		this.selectedHeadRotation = value;
	}

  onSelectedHeadInclination(value:string): void {
		this.selectedHeadInclination = value;
	}

  private showSnackBarErrorMsg(msg: string) {
    this.snackBar.open(msg, '', {
      duration: 5000
    });
  }
}

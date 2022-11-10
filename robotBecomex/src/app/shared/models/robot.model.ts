import { Elbow } from "./elbow.model"
import { Head } from "./head.model"
import { Wrist } from "./wrist.model"

export class Robot {
    public head!: Head
    public leftElbow!: Elbow
    public rightElbow!: Elbow
    public leftWrist!: Wrist
    public rightWrist!: Wrist
}
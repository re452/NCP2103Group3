# PawGress
*Where Fitness meets Friendship.*

### System Overview
  PawGress is an interactive exercise and wellness tracking system that helps users manage their
fitness journey through gamification. The application allows users to create, organize, and track custom
workout routines and wellness tasks while nurturing a virtual streak pet that evolves as users maintain
consistent exercise habits. By completing daily fitness challenges and maintaining workout streaks, users
can level up their companion pet, creating an engaging motivational system that encourages long-term
health commitment.

### System Purpose
  PawGress is designed for a universal audience, catering to anyone seeking to improve their fitness
consistency and organizational habits. While highly beneficial for students managing academic wellness
and professionals balancing work-life fitness, the application is equally valuable for casual users, fitness
enthusiasts, and individuals pursuing personal health goals. Its intuitive design ensures accessibility for
users of all technical backgrounds, making structured exercise management achievable for everyone.

### Main Features
- Create and customize personal exercise routines and wellness tasks.
- Log workout progress with details like duration, intensity, and completion status.
- Visualize consistency and achievements through an interactive progress dashboard.
- Nurture a unique virtual pet that levels up and evolves with maintained workout streaks.
- Receive a daily, motivational quote tailored to foster perseverance.

### Purpose & Benefits
  PawGress transforms the often solitary challenge of maintaining an exercise regimen into an
engaging and rewarding experience. The system is designed to enhance user organization, boost
efficiency in workout planning, and significantly increase productivity in achieving fitness goals. By
integrating a nurturing virtual pet that thrives on user consistency, PawGress provides a powerful
motivational tool that lessens the difficulty of tracking progress. This gamified approach not only
improves task prioritization but also fosters a sustainable commitment to a healthier lifestyle by making
fitness management both effective and enjoyable.

## Application of OOP Concepts

**ENCAPSULATION - Data Protection & Controlled Access**
- Making sensitive fields private (User.Password, User.Email, Pet.Experience)
- Providing public methods with validation for controlled access
- Hiding complex internal logic in private methods

**INHERITANCE - Code Reuse & Hierarchy**
Creating Challenge as base class with common properties
Having PremadeChallenge and CustomChallenge inherit shared functionality
Using BaseEntity for common ID and timestamp properties

**POLYMORPHISM - Flexible Behaviors**
- Using abstract methods that children implement differently
- Creating interfaces for common behaviors across different classes
- Treating different objects uniformly through base class references

**ABSTRACTION - Hiding Complexity**
- Defining abstract classes with method contracts
- Hiding complex implementation details from users
- Providing simple interfaces for complex operations

**COHESION - Focused Responsibilities**
- Giving each class a single, clear responsibility
- Keeping methods focused on one specific task
- Separating concerns into different classes

**COUPLING - Minimal Dependencies**
- Depending on abstractions (interfaces) not concrete classes
- Using dependency injection in Blazor components
- Keeping classes independent and focused

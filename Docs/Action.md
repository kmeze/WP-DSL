# Action concept

## Description

Creates (PHP) function that than be called in other concepts like [ActivationHook / DeactivationHook / UninstallHook](ActivationAction-DeactivationAction-UninstallAction.md).

## Usage

```
Action <plugin>.<name> <script>
```

## Example

```
// HelloWorld.rhe

WPPlugin HelloWorld
{
    Action GetHello ' return "Hello World!"; ';
}
```

### Generated code

```php
<?php

// ...

function HelloWorld_GetHello() {
     return "Hello World!"; 
}

// ,,,
```

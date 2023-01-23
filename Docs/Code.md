# Code concept

## Description

Inserts PHP code into main plugin file.

## Usage

```
Code <script>
```

## Example

```
// HelloWorld.rhe

WPPlugin HelloWorld
{
    Code 'add_action( "admin_notices", function () {
        ?>
            <div class="notice notice-success is-dismissible">
                <p><?php echo "Hello World!" ?></p>
            </div>
        <?php
    } );';
}
```

### Generated code

```php
<?php

// ...

add_action( "admin_notices", function () {
	?>
        <div class="notice notice-success is-dismissible">
            <p><?php echo "Hello World!" ?></p>
        </div>
	<?php
} );

// ,,,
```

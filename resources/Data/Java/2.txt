import java.awt.*;

public class Arena {

	/**
	 * The size of an arena
	 */
	private static final int DIMENSIONS = 10;
	/**
	 * Constants representing directions
	 */
	private static final int NORTH = 0;
	private static final int EAST = 1;
	private static final int SOUTH = 2;
	private static final int WEST = 3;
	/**
	 * Flag constants
	 */
	private static final int SOUTH_BLOCKED = 0x2;
	private static final int WEST_BLOCKED = 0x4;
	/**
	 * Map information
	 */
	public int[][] flags;
	private int startX;
	private int startY;
	private int endX;
	private int endY;

	public Arena(int startX, int startY, int endX, int endY, int[][] flags) {
		this.startX = startX;
		this.startY = startY;
		this.endX = endX;
		this.endY = endY;
		this.flags = flags;
	}

	/**
	 * Gets the point in which you would stop moving
	 * in the given direction from these coordinates
	 */
	private Point getEndPoint(int x, int y, int direction) {
		switch (direction) {
			case NORTH:
				while (y > 0 && (flags[x][y - 1] & SOUTH_BLOCKED) == 0) y--;
				break;
			case SOUTH:
				while (y < DIMENSIONS - 1 && (flags[x][y] & SOUTH_BLOCKED) == 0) y++;
				break;
			case EAST:
				while (x < DIMENSIONS - 1 && (flags[x + 1][y] & WEST_BLOCKED) == 0) x++;
				break;
			case WEST:
				while (x > 0 && (flags[x][y] & WEST_BLOCKED) == 0) x--;
				break;
			default:
				throw new IllegalArgumentException("Invalid Direction: " + direction);
		}
		return new Point(x, y);
	}

	/**
	 * Render this arena on the given graphics object
	 *
	 * @param size the size of each tile
	 */
	public void paint(Graphics g, int size) {
		int tileSize = DIMENSIONS * size;
		// Draw Start/End
		g.setColor(new Color(255, 150, 0, 150));
		g.fillRect(startX * size, startY * size, size, size);
		g.setColor(new Color(0, 255, 0, 150));
		g.fillRect(endX * size, endY * size, size, size);
		// Draw Grid
		g.setColor(Color.DARK_GRAY);
		for (int x = 0; x < DIMENSIONS; x++) g.drawLine(x * size, 0, x * size, tileSize);
		for (int y = 0; y < DIMENSIONS; y++) g.drawLine(0, y * size, tileSize, y * size);
		// Draw Boundaries
		g.setColor(Color.RED);
		for (int horizontal = 0; horizontal < DIMENSIONS; horizontal++) {
			for (int vertical = 0; vertical < DIMENSIONS; vertical++) {
				int x = horizontal * size;
				int y = vertical * size;
				if ((flags[horizontal][vertical] & SOUTH_BLOCKED) > 0)
					g.drawLine(x, y + size, x + size, y + size);
				if ((flags[horizontal][vertical] & WEST_BLOCKED) > 0)
					g.drawLine(x, y, x, y + size);
			}
		}
	}

}